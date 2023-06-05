# -*- coding: utf-8 -*-
"""PTE-Testing.ipynb

Automatically generated by Colaboratory.

Original file is located at
    https://colab.research.google.com/drive/1eGrKb5hPPDm_XmhwY01DHEOUm--gZXaf
"""

from google.colab import drive

drive.mount("/content/drive")

!pip install ffmpeg-python

import os
import pickle
import librosa.display
import numpy as np
import ffmpeg as ff
from typing import List, Tuple
import random
import cv2
import warnings
import tensorflow as tf
from sklearn.metrics import mean_squared_error, mean_absolute_error
from sklearn.model_selection import KFold
warnings.filterwarnings("ignore")

def extract_audio_from_video(file_path: str) -> np.ndarray:
    """
    Extract the audio from the video.
    :param file_path: The video file path.
    :return: The audio data.
    """

    inputfile = ff.input(file_path)
    out = inputfile.output('-', format='f32le',
                           acodec='pcm_f32le', ac=1, ar='44100')
    raw = out.run(capture_stdout=True)
    del inputfile, out
    return np.frombuffer(raw[0], np.float32)


def preprocess_audio_series(raw_data: np.ndarray) -> np.ndarray:
    """
    Preprocess the audio series.
    :param raw_data: The raw audio data.
    :return: The preprocessed audio data.
    """

    # Set the number of MFCC(N) and the number of frames(M) to be extracted from the audio data.
    N, M = 24, 1319

    # Extract MFCC from the audio data and convert it to a numpy array.
    mfcc_data = librosa.feature.mfcc(y=raw_data, n_mfcc=N)

    # Standardize MFCC (zero mean and unit variance)
    mfcc_data_standardized = (
        mfcc_data - np.mean(mfcc_data, axis=1, keepdims=True)) / np.std(mfcc_data, axis=1, keepdims=True)

    # Pad the MFCC data with zeros to make it of shape (N, M)
    if mfcc_data_standardized.shape[1] < M:
        padding = np.zeros((N, M - mfcc_data_standardized.shape[1]))
        padded_data = np.hstack((mfcc_data_standardized, padding))
    else:
        padded_data = mfcc_data_standardized[:, :M]

    return padded_data


def get_number_of_frames(file_path: str) -> int:
    """
    Get the number of frames in the video.
    :param file_path: The video file path.
    :return: The number of frames in the video.
    """

    # Get the number of frames in the video.
    probe = ff.probe(file_path)
    video_streams = [stream for stream in probe["streams"]
                     if stream["codec_type"] == "video"]
    # width = video_streams[0]['coded_width']
    # height = video_streams[0]['coded_height']
    del probe
    return video_streams[0]['nb_frames']


def extract_N_video_frames(file_path: str, number_of_samples: int = 6) -> List[np.ndarray]:
    """
    Extract N video frames from the video.
    :param file_path: The video file path.
    :param number_of_samples: The number of video frames to be extracted.
    :return: The list of extracted video frames.
    """

    # Get the number of frames in the video.
    nb_frames = int(get_number_of_frames(file_path=file_path))

    video_frames = []
    random_indexes = random.sample(range(0, nb_frames), number_of_samples)

    # Extract the video frames.
    cap = cv2.VideoCapture(file_path)
    for ind in random_indexes:
        cap.set(1, ind)
        _, frame = cap.read()
        video_frames.append(cv2.cvtColor(frame, cv2.COLOR_BGR2RGB))
    cap.release()
    del cap, random_indexes
    return video_frames


def resize_image(image: np.ndarray, new_size: Tuple[int, int]) -> np.ndarray:
    """
    Resize the image.
    :param image: The image to be resized.
    """
    return cv2.resize(image, new_size, interpolation=cv2.INTER_AREA)


def crop_image_window(image: np.ndarray, training: bool = True) -> np.ndarray:
    """
    Crop the image window.
    :param image: The image to be cropped.
    :param training: Whether the image is for training or not.
    :return: The cropped image.
    """

    # Crop the image window to be of size (128, 128).
    height, width, _ = image.shape
    if training:
        MAX_N = height - 128
        MAX_M = width - 128
        rand_N_index, rand_M_index = random.randint(
            0, MAX_N), random.randint(0, MAX_M)
        return image[rand_N_index:(rand_N_index+128), rand_M_index:(rand_M_index+128), :]
    else:
        N_index = (height - 128) // 2
        M_index = (width - 128) // 2
        return image[N_index:(N_index+128), M_index:(M_index+128), :]


def preprocessing_input(file_path: str, training: bool = True) -> Tuple[np.ndarray, np.ndarray, np.ndarray]:
    """
    Preprocess the input data.
    :param file_path: The video file path.
    :param training: Whether the data is for training or not.
    :return: The preprocessed audio, video and label data.
    """
    
    # Audio
    extracted_audio_raw = extract_audio_from_video(file_path=file_path)
    preprocessed_audio = preprocess_audio_series(raw_data=extracted_audio_raw)

    # Video
    sampled = extract_N_video_frames(file_path=file_path, number_of_samples=6)
    resized_images = [resize_image(
        image=im, new_size=(248, 140)) for im in sampled]
    cropped_images = [crop_image_window(
        image=resi, training=training) / 255.0 for resi in resized_images]
    preprocessed_video = np.stack(cropped_images)

    del extracted_audio_raw, sampled, resized_images, cropped_images
    return (preprocessed_audio, preprocessed_video)


def model_load(json_path, model_path):
    """
    Load the model.
    :param json_path: The path to the json file.
    :param model_path: The path to the model file.
    :return: The loaded model.
    """

    cnn = tf.keras.applications.VGG16(weights="imagenet", include_top=False, pooling='max')
    cnn.trainable = False

    json_file = open(json_path, 'r')
    loaded_model_json = json_file.read()
    json_file.close()

    loaded_model = tf.keras.models.model_from_json(loaded_model_json)
    loaded_model.load_weights(model_path)
    return loaded_model


def get_predictions(video_path: str = None):
    """
    Get the predictions for the video.
    :param video_path: The video file path.
    :return: The predictions for the video.
    """
    
    test_data = preprocessing_input(
        file_path=video_path, training=False)

    json_path = '/content/drive/MyDrive/Automatic Recruitment System/model/model.json'
    model_path = '/content/drive/MyDrive/Automatic Recruitment System/model/audio_video_weights.h5'
    combined_network = model_load(json_path, model_path)

    y_pred = combined_network.predict(
        [np.expand_dims(test_data[0], axis=0), np.expand_dims(test_data[1], axis=0)])

    preds = {}
    keys = ['extraversion', 'neuroticism',
            'agreeableness', 'conscientiousness', 'openness']
    for i, key in enumerate(keys):
        preds[key] = float(y_pred[0][i])
    return preds

def cross_val_test(x_test, y_test, n_splits=5):
    # Paths to the model files.
    json_path = '/content/drive/MyDrive/Automatic Recruitment System/model/model.json'
    model_path = '/content/drive/MyDrive/Automatic Recruitment System/model/audio_video_weights.h5'

    # Load the model.
    model = model_load(json_path, model_path)

    # Prepare the KFold cross-validator.
    kf = KFold(n_splits=n_splits)

    # Track mean squared error, mean absolute error and 1-MAE scores across folds.
    mse_scores = []
    mae_scores = []
    one_minus_mae_scores = []

    # Iterate through each fold.
    for _, test_index in kf.split(x_test):
        # Split the data into testing sets for this fold.
        x_test_fold = [x_test[i] for i in test_index]

        voice_test_fold = np.array([item[0] for item in x_test_fold])
        video_test_fold = np.array([item[1] for item in x_test_fold])

        labels_test = np.array([y_test[i] for i in test_index])

        # Predict with the model on the testing data.
        predictions = model.predict([voice_test_fold, video_test_fold])

        # Compute mean squared error for this fold.
        mse = mean_squared_error(labels_test, predictions)

        # Compute mean absolute error for this fold.
        mae = mean_absolute_error(labels_test, predictions)

        # Compute 1-MAE for this fold.
        one_minus_mae = 1 - mae

        # Add to list of scores.
        mse_scores.append(mse)
        mae_scores.append(mae)
        one_minus_mae_scores.append(one_minus_mae)

    # Compute the average MSE, MAE and 1-MAE across all folds.
    avg_mse = np.mean(mse_scores)
    avg_mae = np.mean(mae_scores)
    avg_one_minus_mae = np.mean(one_minus_mae_scores)

    print("-" * 30)
    print(f"Average MSE across {n_splits}-Fold cross-validation: {avg_mse}")
    print(f"Average MAE across {n_splits}-Fold cross-validation: {avg_mae}")
    print(f"Average 1-MAE across {n_splits}-Fold cross-validation: {avg_one_minus_mae}")
    print("-" * 30)

if __name__ == '__main__':
    # Load the ground truth data for the test set.
    gt = pickle.load(
        open("/content/drive/MyDrive/Automatic Recruitment System/annotation_test.pkl", "rb"), encoding='latin1')

    # New dictionary with the video name as the key and the trait values as a list.
    new_gt = {}

    # Iterate over the traits
    for trait, videos in gt.items():
        if trait != 'interview':
          # Iterate over the videos
          for video, value in videos.items():

              # If the video is not in the new dictionary, add it
              if video not in new_gt:
                  new_gt[video] = []

              # Append the value to the new dictionary
              new_gt[video].append(value)

    x_test_path = "/content/drive/MyDrive/Automatic Recruitment System/x_test.pkl"
    y_test_path = "/content/drive/MyDrive/Automatic Recruitment System/y_test.pkl"
    if os.path.exists(x_test_path) and os.path.exists(y_test_path):
        # Load x_test and y_test from the existing pickles
        with open(x_test_path, "rb") as f:
            x_test = pickle.load(f)

        with open(y_test_path, "rb") as f:
            y_test = pickle.load(f)
        
        x_test = np.array(x_test)
        y_test = np.array(y_test)
            
    else:
      x_test = []
      y_test = []

      test_path = '/content/drive/MyDrive/Automatic Recruitment System/test_data/'
      for video_name, values in new_gt.items():
          # Iterate over the test set folders
          for i in range(1, 14):
              # Iterate over the videos in the folder
              for vn in os.listdir(test_path + 'test80_' + str(i).zfill(2)):
                  if video_name == vn:
                      # Get the predictions for the video
                      video_path = test_path + 'test80_' + str(i).zfill(2) + '/' + vn

                      x_test.append(preprocessing_input(file_path=video_path, training=False))
                      y_test.append(new_gt[video_name])

      # Save x_test and y_test as pickles
      with open(x_test_path, "wb") as f:
          pickle.dump(x_test, f)

      with open(y_test_path, "wb") as f:
          pickle.dump(y_test, f)

    cross_val_test(x_test, y_test)
    cross_val_test(x_test, y_test, 10)
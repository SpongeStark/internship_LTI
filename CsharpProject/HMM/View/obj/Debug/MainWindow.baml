   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5   	       View.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:View     5   	     � Title$	  HMM��5   	   $	��450��6   $	��600��6"   .��5	      � .+�5   
   �� $,��&Input the data files (*.csv or *.txt):��6   $
��Left=�6A   $��	38,27,0,0q�6\   $	��Top=�6o   �� 5   
   $��Initial States��6   $
��Left=�6)   $	��Top=�6D   $��	38,67,0,0q�6\   �� 5   
   -   5         ��Name$ btn_OpenFile_InitStates��5      $��Select File��62   $
��Left=�6H   $	��Top=�6c   $��
138,72,0,0q�6{   6�   �� 5   
   -   5        $��lbl_OpenFile_InitStates��5      $��No File��6/   $
��Left=�6B   $	��Top=�6]   $��
208,67,0,0q�6u   6�   �� 5   
   $��Transition Probabilities��6   $
��Left=�63   $	��Top=�6N   $��
38,107,0,0q�6f   �� 5   
   -   5        $ btn_OpenFile_TransProbs��5      $��Select File��62   $
��Left=�6H   $	��Top=�6c   $��198,112,0,0q�6{   6�   �� 5   
   -   5        $��lbl_OpenFile_TransProbs��5      $��No File��6/   $
��Left=�6A   $	��Top=�6\   $��268,107,0,0q�6t   6�   �� 5   
   $��Emission Probabilities��6   $
��Left=�61   $	��Top=�6L   $��
38,147,0,0q�6d   �� 5   
   -   5        $ ��btn_OpenFile_EmissionProbs��5      $��Select File��63   $
��Left=�6I   $��208,152,0,0q�6d   $	��Top=�6y   $��75��6�   6�   �� 5   
   -   5        $ ��lbl_OpenFile_EmissionProbs��5      $��No File��62   $
��Left=�6D   $��288,147,0,0q�6_   $	��Top=�6t   6�   �� 5   
   $��Observations��6   $
��Left=�6'   $	��Top=�6B   $��
38,187,0,0q�6Z   �� 5   
   -   5        $��btn_OpenFile_observation��5      $��	Open File��61   $
��Left=�6E   $��208,192,0,0q�6`   $	��Top=�6u   $��75��6�   6�   �� 5   
   -   5        $��lbl_OpenFile_observation��5      $��No File��60   $
��Left=�6B   $��288,187,0,0q�6]   $	��Top=�6r   6�   �� 5"   
   -	   5        $ btn_FB��5"      $��Forward/Backward��6!   $
��Left=�6<   $��
38,276,0,0q�6W   $	��Top=�6k   $	��120��6�   6�   �� 5#   
   -
   5        $ Terminal��5#      $��
Baum-Welch��6#   $
��Left=�68   $��198,276,0,0q�6S   $	��Top=�6h   $	��132��6�   6�   �� 5$   
   -   5        $ btn_Viterbi��5$      $��Viterbi��6&   $
��Left=�68   $��368,276,0,0q�6S   $	��Top=�6h   $	��106��6�   6�   �� 5%   
   -   5         ��Name$ txt_MaxIters��5%      $
��Left=�6(   $��23��6C   $��268,251,0,0q�6O    �� TextWrapping$
 Wrap=�6d   $	��100��6x   $	��Top=�6�   $��62��6�   $��Center=�6�   �� 5&   
   $��
Max Iters:��6   $
��Left=�6%   $��193,251,0,0q�6@   $	��Top=�6U   5)      5*      
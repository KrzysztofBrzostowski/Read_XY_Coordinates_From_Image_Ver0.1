   M S B A M L   `   `   `  ����  ReadXYCoordinatesFromImage0    *ReadXYCoordinatesFromImage.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$7  1Read (x,y) coordinates from the graphical diagram��5   	   $	��550��5   	   $	��750��6   .��5      ]� .+�5   
   o� $	��Top=�6   $��23��6$   .@�5      m� $��_File��6   .@�5	      m� $
��Exit��6    m� Command 6)   �� *ExitCommand+5
      m� 5      $��_Help��6   .@�5      m� $��WARNING��6    6,   �� *WarningCommand+5      5      �� 5   
   $	��Top=�6   $��60��6*    �� Orientation$ 
Horizontal=�66   .+�5      �� $��Load Image File��6;   $	��140��6U   $
��10,5q�6a   ��6   �� *LoadImageCommand+6a   �� 5      $��Save (x,y) coordinates��6k   $	��140��6�   $
��10,5q�6�   ��6   �� *SaveCommand+{�66   �� * ImageModel.IsSaveButtonEnabled+6�   5      � 5   
    � ColumnDefinitions 5      �� 5      $	��160 �6#   �� 5      $
��Auto �6#   .+�5      �� $ Vertical=�6   $��0��60   .+�5      �� -   5         ��Name$ label1��5      $��30��6(   $	��150��64   $ ��Loaded (x,y) coordinates :��6@   6g   �� 5      -   5        $��textBox1��5   '   $	��370��6   $��	10,0,10,0q�68    �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�6]    �� AcceptsReturn .  �� TextWrapping$ NoWrap=�6�   �� 5      -   5        $ label2��5      $��30��6(   $	��140��64   ��6@   �� *ImageModel.NumberOfTxtRow+5      � 5"      $��1��6   $��10,30q�6#   .+�5#      �� 
����+��$��6q�62   ._�5$      �� -   5        $��ImageViewer1��5$      $	��300��60   $	��400��6@   5%      5&      5'      5+      5,      
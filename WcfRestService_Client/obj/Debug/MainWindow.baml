   M S B A M L   `   `   `  ����P  LWcfRestService_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WcfRestService_ClientWcfRestService_Client  5   	   &     WcfRestService_Client.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:WcfRestService_Client     5   	     � Title$Q  KЗапрос сведений об объекте из Росреестра��5   	   $	��350��6:   $��374.595��6G   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��1* �6   .+�5   
   �� $��Stretch=�6   $	��Top=�63   $��0��6K   .@�5      �� $"��Поиск по номеру��6   .��5      �  5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5      �� -   5         ��Name$ 
textNumber��5      $��Stretch=�62   $��23��6P   $��10,10,10,10q�6\    �� TextWrapping$
 Wrap=�6q   $"��Введите сюда КН��6�   $	��Top=�6�   �� 5      -   5         ��Name$ buttonNumSearch��5      $(��"Запрос в Росреестр��66   $��Stretch=�6S   $��
10,0,10,10q�6q   $	��Top=�6�   $��1��6�   5      5      �� 5      $"��Поиск по адресу��6   .��5      �  5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $��1* �6(   .+�5!      �� -   5         ��Name$ comboMacroRegion��5!      $��10,10,10,10q�69   $��0��6N   6�   �� 5"      -   5         ��Name$ labelMacroPlaceHolder��5"      $
��Left=�6;   $-��'Выберите макрорегион��6V   $��10,10,10,10q�6u   $��0��6�   �� 5#      -   5        $ 
textStreet��5#      $��Stretch=�62   $��23��6P   $��
10,0,10,10q�6\   $
 Wrap=�6p   $0��*Введите название улицы��6�   $	��Top=�6�   $��1��6�   �� 5$      -   5        $ 	textHouse��5$      $��Stretch=�61   $��23��6O   $��
10,0,10,10q�6[   $
 Wrap=�6o   $(��"Введите номер дома��6�   $	��Top=�6�   $��2��6�   �� 5%      -   5        $ buttonAddrSearch��5%      $(��"Запрос в Росреестр��67   $��Stretch=�6T   $��
10,0,10,10q�6r   $	��Top=�6�   $��3��6�   5&      5'      5(      R� 5)   
   -   5         R�Name$ treeView��5)      $��Stretch=�6%   $��
10,0,10,10q�6C   $��Stretch=�6W   $��1��6s   5*      5+      
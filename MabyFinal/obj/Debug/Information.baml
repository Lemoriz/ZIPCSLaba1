   M S B A M L   `   `   `  ����D  @MabyFinal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MabyFinal	MabyFinal  5   	       MabyFinal.Information   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MabyFinal     5   	   -   5          � Title$  Information��5   	   $	��400��6   $	��500��6*    � WindowStartupLocation$ CenterScreen=�67   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��34* �6   6+   �� 5      $	��33* �6   6+   �� 5      $
��109* �6   6,   �� 5      $	��31* �6   6+   �� 5      $
��130* �6   6,   �� 5      $	��32* �6    � ColumnDefinitions 5   
   �� 5      $
��217* �6   6.   �� 5      $
��275* �6   6.   .+�5   
   �� $
��Left=�6   $��94,6,0,0q�6/   $
��Wrap=�6A   $	��Top=�6U   $	��107��6m   $��16��6y   �� 6�   $�	Username:��6�   �� 6�   �� 6�   6�   �� 5   
   $
��Left=�6   $��69,7,0,0q�6/   $
��Wrap=�6A   $��Levels of your access:��6U   $	��Top=�6s   $	��138��6�   $��1��6�   $��16��6�   �� 5   
   $
��Left=�6   $��56,8,0,0q�6/   $
��Wrap=�6A   $��Rights of all users:��6U   $	��Top=�6q   $	��149��6�   $��3��6�   $��16��6�   �� 5   
   -   5        $��userName��5   U   $
��Left=�6   $��12,5,0,0q�6/   $
��Wrap=�6A   $	��Top=�6e   $��87��6}   $��1��6�   $��16��6�   &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ grida��5!      $��2��6"   $��2��60      AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 225 6_   $
��13,0q�6q      Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ Object��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 66   �� *
nameObject+  5%      $ Level access��6%   	 6;   �� *
nameAccess+5(        5*   
   -   5        $��grida2��5*      $��4��6!   $��2��6.    . $��	13,3,11,4q�6]    5,      
   RowDetailsTemplate
 56      �� 57      5H      5J      �� 5L   
   -   5        $��ClickedItemDisplay��5L   P   $��10,10,46,-3q�6   $��5��6+   $��Bottom=�68   6P   5N      5O      
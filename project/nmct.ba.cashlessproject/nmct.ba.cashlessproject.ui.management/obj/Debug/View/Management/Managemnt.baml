   M S B A M L   `   `   `  ����`  \nmct.ba.cashlessproject.ui.management, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:nmct.ba.cashlessproject.ui.management.ViewModel/nmct.ba.cashlessproject.ui.management.ViewModel  5   	   }Cclr-namespace:nmct.ba.cashlessproject.ui.management.View.Management5nmct.ba.cashlessproject.ui.management.View.Management  5   	   E    ?nmct.ba.cashlessproject.ui.management.View.Management.Managemnt   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Lvm=clr-namespace:nmct.ba.cashlessproject.ui.management.ViewModel     5   	   QvCclr-namespace:nmct.ba.cashlessproject.ui.management.View.Management     5   	     � Title$  	Managemnt��5   	   $	��720��6   $
��1280��6(    � DataContext 5      C  =nmct.ba.cashlessproject.ui.management.ViewModel.ApplicationVM  5	   
    � 	Resources 5       �� DataType?  9nmct.ba.cashlessproject.ui.management.ViewModel.AccountVMC   =nmct.ba.cashlessproject.ui.management.View.Management.Account@  :nmct.ba.cashlessproject.ui.management.ViewModel.ProductsVMD   >nmct.ba.cashlessproject.ui.management.View.Management.ProductsA  ;nmct.ba.cashlessproject.ui.management.ViewModel.EmployeesVME   ?nmct.ba.cashlessproject.ui.management.View.Management.EmployeesD  >nmct.ba.cashlessproject.ui.management.ViewModel.CashregisterVMH	   Bnmct.ba.cashlessproject.ui.management.View.Management.CashregisterA
  ;nmct.ba.cashlessproject.ui.management.ViewModel.CustomersVME   ?nmct.ba.cashlessproject.ui.management.View.Management.CustomersB  <nmct.ba.cashlessproject.ui.management.ViewModel.StatisticsVMF   @nmct.ba.cashlessproject.ui.management.View.Management.Statistics��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)(�� �     5      *,
 +)(�� �     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5      	  5      �� 5   
   " 
 6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5       �  � ColumnDefinitions 5!   
   �� 5"      $	��29* �6   �� 5#      $
��160* �6   �� 5$      $
��160* �6   �� 5%      $
��160* �6   �� 5&      $
��160* �6   �� 5'      $
��160* �6   �� 5(      $
��160* �6   �� 5)      $
��255* �6   �� 5*      $	��30* �6    � RowDefinitions 5,   
   �� 5-      $	��44* �6   �� 5.      $	��62* �6   �� 5/      $
��514* �6   �� 50      $	��29* �6   .+�53   
   �� $��User :��6   $��1��6!   $��Center=�61   $
��60,9q�6N   $��Center=�6\   $��26��6w   $��40��6�   �� 54   
   $��2��6   $
��Left=�6$   $��
10,14,0,14q�6?   $��1��6S   $
��Wrap=�6d   $��USERNAME��6x   $��Center=�6�   $��16��6�   $	��150��6�   �� 56   
   $��0,0,0,0q�65   $��1��6F   $��1��6S   $��6��6c   ��6   �� *Pages+��57      �� 58      .R�59      �� $	��140��66   $��42��6B   $��19,0,0,0q�6N   ��6   �� *Name+��6`   �� *DataContext.ChangePageCommand+ �� RelativeSource ��  �� AncestorType" ���6�   �� 5:      ��5<      �� 5=      .R�5>      ��  �� Orientation$ 
Horizontal=�6!   5?      5A      �� 5B   
   $��2��69   $��1��6F   $��7��6V   ��6   �� *CurrentPage+6l   �� 5C   
   $��logout��6   $��7��6"   $��Center=�62   $��Center=�6P   $��76��6k   5M      5N      
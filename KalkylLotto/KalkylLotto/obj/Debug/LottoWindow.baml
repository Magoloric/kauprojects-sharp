   M S B A M L   `   `   `  ����F  BKalkylLotto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:KalkylLottoKalkylLotto  5   	       KalkylLotto.LottoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:KalkylLotto     5   	     � Title$  	Lotteriet��5   	   $��263.111��6   $��320.666��6,   ���	#FF526E42.��5	      � .+�5
   
   �� $1��+Ange ditt lottorad (Siffror mellan 1 och 35��6   $
��Left=�6F   $��	40,13,0,0q�6a   $	��Top=�6t   
�������$	��240��6�   �� 5   
   -   5         ��Name$ UserLottoNum1��5      $
��Left=�6)   $��23��6D   $��	11,44,0,0q�6P    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $��38��6�   ���	#FF84DE886�   �� 5   
   -   5        $ UserLottoNum2��5      $
��Left=�6)   $��23��6D   $��	54,44,0,0q�6P   $
 Wrap=�6c   $	��Top=�6w   $��37��6�    J� RenderTransformOrigin$ 1.556,0.478&�6�   ���	#FF84DE886�   �� 5   
   -   5        $ UserLottoNum3��5      $
��Left=�6)   $��23��6D   $��	96,44,0,0q�6P   $
 Wrap=�6c   $	��Top=�6w   $��37��6�   ���	#FF84DE886�   �� 5   
   -   5        $ UserLottoNum4��5      $
��Left=�6)   $��23��6D   $��
138,44,0,0q�6P   $
 Wrap=�6d   $	��Top=�6x   $��39��6�   $ 1.426,0.594&�6�   ���	#FF84DE886�   �� 5   
   -   5        $ UserLottoNum5��5      $
��Left=�6)   $��23��6D   $��
182,44,0,0q�6P   $
 Wrap=�6d   $	��Top=�6x   $��37��6�   ���	#FF84DE886�   �� 5   
   -   5        $ UserLottoNum6��5      $
��Left=�6)   $��23��6D   $��
224,44,0,0q�6P   $
 Wrap=�6d   $	��Top=�6x   $��38��6�   ���	#FF84DE886�   �� 5   
   -   5        $ UserLottoNum7��5      $
��Left=�6)   $��23��6D   $��
267,44,0,0q�6P   $
 Wrap=�6d   $	��Top=�6x   $��37��6�   $ 2,0.623&�6�   ���	#FF84DE886�   �� 5   
   $��Ange antal dragningar��6   $
��Left=�60   $��	54,81,0,0q�6K   $	��Top=�6^   
�������$	��136��6�   �� 5   
   -   5        $ NrOfDrawsField��5      $
��Left=�6*   $��23��6E   $��
195,84,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $��67��6�   ���	#FF84DE886�   �� 5   
   -	   5         ��Name$ _5RightTextBlock��5      $
��Left=�6.   $��
20,194,0,0q�6I   $
��Wrap=�6]   $	��Top=�6q   $��79��6�   $��23��6�   {�.  
����������	#FF198239��6�   �� *
FiveRights+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWay=�6  �� 5   
   $��5 rätt��6   $
��Left=�6!   $��
20,163,0,0q�6<   $	��Top=�6P   
�������6h   �� 5   
   -
   5        $ _6RightTextBlock��5      $
��Left=�6.   $��118,194,0,0q�6I   $
��Wrap=�6^   $	��Top=�6r   $��79��6�   $��23��6�   {�.  
����������	#FF198239��6�   �� *	SixRights+$ PropertyChanged=�$ OneWay=�6  �� 5   
   $��6 rätt��6   $
��Left=�6!   $��118,163,0,0q�6<   $	��Top=�6Q   
�������6i   �� 5   
   -   5        $ _7RightTextBlock��5      $
��Left=�6.   $��217,194,0,0q�6I   $
��Wrap=�6^   $	��Top=�6r   $��79��6�   $��23��6�   {�.  
����������	#FF198239��6�   �� *SevenRights+$ PropertyChanged=�$ OneWay=�6  �� 5   
   $��7 rätt��6   $
��Left=�6!   $��217,163,0,0q�6<   $	��Top=�6Q   
�������6i   �� 5   
   -   5         ��Name$ StartLottoButton��5      $��Kör!��6+   $
��Left=�6:   $��118,123,0,0q�6U   $	��Top=�6j   $��79��6�   $��35��6�   ���	#FF65FF71���	#FF004D06
���   �6�   5      5      
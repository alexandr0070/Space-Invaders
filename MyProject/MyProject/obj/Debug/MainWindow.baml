   M S B A M L   `   `   `  ????D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	       MyProject.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)    ? 
ResizeMode$ NoResize=?65   .??5	      ? .+?5
   
   ?? -   5         ??Name$ btn_game??5
      $??Играть??6#   $
??20px(?64   $??Arial)?6D   $??Center=?6W   $??Center=?6t   $??
0, 0, 0, 0q?6?   $	??300??6?   6?   ?? 5   
   -   5        $ 	btn_close??5      $??Закрыть??6$   $
??20px(?66   $??Arial)?6F   $??Center=?6Y   $??Center=?6v   $??0, 150, 0, 0q?6?   $	??300??6?   6?   ?? 5   
   -   5         ??Name$ slider_volume??5      $??Center=?6)   $??	0,359,0,0q?6F   $	??Top=?6Y   $	??400??6q    ? Minimum$ 0Q?6}    ? Maximum$	 100Q?6?    ? Value$	 100Q?6?   6?      MyProject.muscontrol  5   
   -   5          Name$ 	btn_start??5         MyProject.muscontrol   MouseDoubleClick$  btn_start_MouseDoubleClick??6v   $
??Left=?6.   $??663,316,0,0q?6I   $	??Top=?6^   6v      MyProject.muscontrol1  5   
   -   5        	  Name$	 	btn_pause??5         MyProject.muscontrol1
   MouseDoubleClick$ 
 btn_pause_MouseDoubleClick??6w   $
??Left=?6/   $??663,377,0,0q?6J   $	??Top=?6_   6w   ?? 5   
   $??Музыка??6   $??Arial)?6!   
??? ???$
??Left=?6H   $??673,274,0,0q?6c   $	??Top=?6x   ?? 5   
   $??Громкость:??6   $??Arial)?6%   
??? ???$
??Left=?6L   $??124,356,0,0q?6g   $	??Top=?6|   5      5      
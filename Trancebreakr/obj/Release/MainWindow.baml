   M S B A M L   `   `   `  ���� G  CTrancebreakr, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TrancebreakrTrancebreakr      Trancebreakr.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Trancebreakr     X TXceed.Wpf.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$  Trancebreakr��$	��450��$	��800��.��� .+��� -    ��Name$ existingScheduledTasks��$
��Left=�$	��161��$��480,149,0,0q�$	��Top=�$	��254��.@��� $3��-Refresh to see your existing scheduled tasks.���� -    ��Name$ description��$����This program is a tool to set automatic shutdown Scheduled Tasks for your PC.
Escape the doomscrolling; Leave thevirtual world and wake up on time tomorrow!��$��Center=�$��	36,22,0,0q�$	��Top=�$	��698��$��57��$��Italic$�$��14(�$��Center=�$��Center=�"  Xceed.Wpf.Toolkit.TimePicker  -     Name$ timeOfDayTextBox��$
��Left=�$��
42,133,0,0q�,  &Xceed.Wpf.Toolkit.Primitives.InputBase   Text$ 11:30 PM��$	��Top=��� -   $ 	timeLabel��$!��Time of day to shut down pc��$��
42,107,0,0q�$	��Top=�$
��Left=�$��0,5,5,5q��� -   $ numMinutesWarningLabel��$*��$Number of minutes beforehand to warn��$
��Left=�$��
42,180,0,0q�$	��Top=�$��0,5,5,5q��� -    ��Name$# numberOfMinutesWarningTextBox��$
��Left=�$��23��$��
42,211,0,0q� �� TextWrapping$
 Wrap=�$��10��$	��Top=�$	��120���� -    ��Name$ setScheduleButton��$��Set Scheduled Tasks��$
��Left=�$��
55,267,0,0q�$	��Top=�$	��170��$��43���� -   $ button��$��Delete scheduled tasks��$
��Left=�$��480,315,0,0q�$	��Top=�$	��136���� -	   $ refreshTasks��$��Refresh tasks��$
��Left=�$��659,124,0,0q�$	��Top=�$��75���� -
   $$ currentScheduledTasksListLabel��$��Trancebreakr Tasks��$
��Left=�$��480,123,0,0q�$	��Top=�
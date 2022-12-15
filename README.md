# Trancebreakr

Ever lose track of time and stay up too late, ruining your next day? 

Does this happen to your kid? Are you in college, spending every night up way too late? 

Break the spell! Take away the need for willpower and let automation take over! Automation is the new discipline! 

Trancebreakr is a program to create Windows Scheduled Tasks that 
1) shuts down the computer at a designated time every night
2) gives a warning x minutes before shutting down. 

Add or delete the Tasks from within the program. 


![trancebreakr_demo](https://user-images.githubusercontent.com/6875673/202711070-5d5ae8ee-ea7e-42bb-8645-90b9c878e0e7.png)

# Troubleshooting

If you uninstalled this app and don't know where to look to remove the shutdown tasks, go to Task Scheduler on your computer and they will be there. 


# Dev notes
Publishing via ClickOnce was broken with a recent commit and I had to create builds using Inno Script instead. I Published to a local folder via VS, copied the program files to a different folder, then compiled that via Inno Script. Much better!

# RefreshRateAutoSwitcher

Auto switch between high/low refresh rate when unplug/plugging in Windows laptop. 

- Use Task Scheduler, does not keep running in background
- Customize refresh rates and monitors
- Use Windows API, should work on all Windows laptop

Require [.NET 6.0 Runtime](https://dotnet.microsoft.com/en-us/download)

## Instruction

1. Download the files from Release. **(Do not delete/move the files after adding the task to Task Scheduler, they are required to work.)**
2. Run the tool, Select the monitor and enter both refresh rates. Then click Save Settings.
3. A Test run is available, if the setting is applied correctly, DISP_CHANGE_SUCCESSFUL should be shown in Result.
4. Use Add task to Task Scheduler for auto switch. It would trigger when the laptop is unplugged/plugged in. When triggered, the tool would launch, change the refresh rate, and close itself if success.
5. Remove the task from Task Scheduler with the tool. Moving the files would require removing and adding the task.

#
Since the ASUS Smart Display Control that came with the laptop has stopped working and not change my display refresh rate when unplug/plugging in. I have decided to make this. I had it fixed* later on, but this should be useful for those laptops that does not comes with the feature.

*I have tried reinstalled the display driver and the Smart Display Control, that didn’t do anything. The fix for me is: Go to Device Manager, on the top, View -> Show hidden device. Uninstall all the greyed-out monitors. Reinstall Smart Display Control.

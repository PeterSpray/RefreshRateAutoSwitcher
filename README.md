# RefreshRateAutoSwitcher


# WIP

Auto switch between high/low refresh rate when unplug/plugging in Windows laptop. Detection of power state change would be done with task scheduler. Configure the following with the exe:

- settings
- add/remove task from task scheduler
- test switch

Since the ASUS Smart Display Control that came with the laptop has stopped working and not change my display refresh rate when unplug/plugging in. I have decided to make this. I had it fixed* later on, but this should be useful for those laptops that does not comes with the feature.

*I have tried reinstalled the display driver and the Smart Display Control, that didn’t do anything. The fix for me is: Go to Device Manager, on the top, View -> Show hidden device. Uninstall all the greyed-out monitors. Reinstall Smart Display Control.

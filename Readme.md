# Volume Meter

## Overview

This is a simple volume meter that only displays when the system volume changes. The program is based on WPF, please make sure your computer can support it.

This program was created primarily for users of Creative SoundBlaster sound cards, because there is a knob on the card to adjust the system volume, but the system won't show a UI contains detailed volume value.

Some older models had a volume meter program included in their drivers, but with the SoundBlaster Commander, Creative has not added this program!

So I've written this program to present a similar experience.

## Usage

Download, compile, then run it!

(I used some Nuget packages to simplify the development, so you need to install them first before compiling.)

**IMPORTANT: The program will add itself to the registry to boot itself, which is the default, non-disable behavior.**

# Color Picker for StreamDeck

## Description

As a [color-blind](https://en.wikipedia.org/wiki/Color_blindness) person sometimes I struggle to recognize certain colors, so I thought it would be nice to have the functionality of [WhatColor](http://www.hikarun.com/e/) in StreamDeck. And while I was there, I decided to add the options to show their RGB or hexadecimal value.

When pressed, it will get the color information (name, RGB, or hexadecimal) of the pixel where the mouse is and show it on the StreamDeck key. Optionally, the the value can be copied to the clipboard

![color-picker](images/color-picker.png).

Also check my [KeePass plugin](https://github.com/VictorGrycuk/StreamDeck-KeePass) for StreamDeck.

It was done using BarRaider's [Stream Deck Tools](https://github.com/BarRaider/streamdeck-tools).



## Features

- Can show three different color values
  - Color name
  - RGB values
  - Hexadecimal value
- Three behaviour modes
  - On key press
  - Dynamic
  - Fixed
- Customizable color name definitions
  - The names are taken from a json file with a list with a color name and its RGB value
  - The default is a slightly modified version of [WhatColor](http://www.hikarun.com/e/)'s recommended setting
  - Also included is the VGA definition also taken from [WhatColor](http://www.hikarun.com/e/) settings
- Values can be copied to the clipboard



## Color Values

### Color Names

![color-picker](images/cycle-name.gif).

This value option will show the name of the color on the key's screen.

The plugin breaks multi words names into new lines, so its best not to use with more than 3 words on it or it wont fit the key's screen It also resizes the font based on the longest word. `Cornflower` can be read, but anything longer and it might be too small to be read.

The colors name definition is stored in a file named `Colors.definition.json`, based on [WhatColor](http://www.hikarun.com/e/)'s recommended setting, found in a folder named ColorsDefinitions at the root folder of the plugin. At the moment, the plugin only loads this file, if you wish to change the definition file, you can either modify this file or replace it.

When the copy to clipboard option is selected, the name of the color will be copied (including new lines).

### Color RGB Value

![color-picker](images/cycle-rgb.gif)

When this value option is selected, it will show the RGB values of the pixel.

When the copy to clipboard option is selected, only the numeric values delimited by a comma will be copied.

### Color Hexadecimal Value

![color-picker](images/cycle-hexa.gif)

When this value option is selected, it will show the hexadecimal value of the pixel.

When the copy to clipboard option is selected, the hexadecimal value without the number sign will be copied to the clipboard.



## Behaviours

### On Key Press

It will update the color value of the pixel where the mouse is at whenever the key is pressed.

It will also copy the value of the color if the option it is activated.

### Dynamic

The key screen will be automatically updated every second based on the position of the mouse. AFAIK, one second is the shortest tick that the Stream Deck API allows.

When the key is pressed, and if the option is selected, the value will be copied to the clipboard.

### Fixed

Similar to Dynamic, except it wont follow the mouse location.

Instead, when the key is pressed the location of the mouse is updated and will stay ready the color of the pixel at that location.

If the copy to clipboard option is activated, it will update the copied value every second.

* * *

The icons are modified version of _Color_ and _iOS Filled_ at [Icon8](https://icons8.com).

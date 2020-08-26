# Color Picker for StreamDeck 
## Description

As a [color-blind](https://en.wikipedia.org/wiki/Color_blindness) person sometimes I struggle to recognize certain colors, so I thought it would be nice to have the functionality of [WhatColor](http://www.hikarun.com/e/) in StreamDeck. And while I was there, I decided to add the options to show their RGB or hexadecimal value.

When pressed, it will get the color information (name, RGB, or hexadecimal) of the pixel where the mouse is and show it on the StreamDeck key. Optionally, the the value can be copied to the clipboard

![color-picker](images/color-picker.png).

Also check my [KeePass plugin](https://github.com/VictorGrycuk/StreamDeck-KeePass) for StreamDeck.

It was done using BarRaider's [Stream Deck Tools](https://github.com/BarRaider/streamdeck-tools).

## Features
#### Color Picker

When pressed, it will return either the color name, the RGB value, or the hexadecimal value of the color where the mouse is. It has the following configuration:

- **Value to show:** The value to show on the key:
  - Color name
  - RGB Value
  - Hex Value
- **Copy value to clipboard:** If checked, it will copy the selected value to the clipboard. For the RGB it will copy the RGB value delimited by coma, for the hexadecimal value it will copy the hexadecimal without the hash character.

#### Custom Color Name

I am using the [WhatColor](http://www.hikarun.com/e/) names definition for the 16 VGA color. I might expand this, not sure.

However, I decided to leave the definition file open for customization. The file containing the definition is called `Colors.json` and can be found in the root folder of the plugin, and it consists of an array of color name and its RGB value.

However, have in mind that in its current state, the plugin has a static font size and long names might be cut off.

## Future Features

I am planning to add two different behaviour for the plugin:

- **Dynamic:** Follow the mouse around and update the data shown in the key every 1 second.
- **Fixed Dynamic:** Same as above, except at a fixed screen location.

---

The icons are modified version of *Color* and *iOS Filled* at [Icon8](https://icons8.com).
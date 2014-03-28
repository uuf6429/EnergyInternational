EnergyInternational
===================

A simple HTA/HTML-based Web Player together with a thin client for tight Desktop integration.

Installation
------------

Just [download the program](https://github.com/uuf6429/EnergyInternational/raw/master/exe/bin/Release/EnergyInternational.exe) and you're ready to go!

Technical Details
-----------------

The main player is a plain HTML file, so in short all the UI and logic is in there. A program was created to act as a better wrapper to the program, providing tray icon support etc.

If you would like to run the HTML file by itself, it is advised to do so by naming the file with a `.hta` extension so that it runs as an [HTML Application](http://en.wikipedia.org/wiki/HTML_Application).

A final note, the list of radio stations actually a public, (non-editable to avoid abuse), Google spreadsheet. The reason for this is two-fold: to avoid storing this on a server somewhere and to still be able to push updates. I used this instead of Github itself because it is easier to integrate it with an HTML file.

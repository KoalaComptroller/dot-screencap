# dot-screencap

###### Features
+ Take screenshots of your primary screen.

 ``` csharp
var screencap = new ScreenCapture();  
screencap.OnScreenshotTaken += Screencap_OnScreenshotTaken; // Optional: Subscribe to the event.
screencap.TakeScreenshot();                                 // Optional: Add a filename.
 ```
+ Record your primary screen and save it as gif.

 ``` csharp
// Experimental stage.
var screencap = new ScreenCapture();
int recordingTime = 5;                            // Time in seconds
screencap.OnGifCreated += Screencap_OnGifCreated; // Optional: Subscribe to the event.
screencap.CreateGIF(recordingTime);
// or
screencap.PERFORMANCETEST_CreateGIF();            // Will record 50 frames in 10 seconds.
 ```

***

###### Documentation
Click [here](http://speisaa.github.io) to see the documentation.

***

###### Planned features
- [x] Take screenshots     (added in v0.1.0)
- [x] Create gifs          (added in v0.1.1)
- [x] Add documentation    (added in v0.1.2)
- [ ] Add examples
- [ ] Change output path
- [ ] Record videos
- [ ] Learn to code :joy:

***

###### How to use it in your solution
Download and build it.  
Add a reference in your solution.

***

###### Final word
If you like the project please **star it** in order to help to spread the word. That way you will make the framework more significant and in the same time you will motivate me to improve it, so the benefit is mutual.

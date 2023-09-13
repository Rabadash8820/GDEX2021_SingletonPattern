# The Singleton Pattern: You're Doing It Wrong

## Overview

This repo contains all source code and assets from my GDEX 2021 presentation _The Singleton Pattern: You're Doing It Wrong_. Watch the [recording](https://1drv.ms/v/s!Apw9vDm6ePFBgupH_BBwqff2WtzR9w?e=JXvZ4p) of the talk for background. Slides are available on [One Drive](https://1drv.ms/p/s!Apw9vDm6ePFBgdJ2ImHZWF45mAWEOg?e=3wzH34).

The Unity project is contained in the `FlappyClone/` folder. It consists of a single scene `scenes/main.unity` with several root GameObjects, the most important three being:

1. `basic-singletons`
2. `static-singletons`
3. `di-singletons`

Activating/deactivating these root objects will toggle which of the three approaches are used to handle dependencies in the script code: basic component references, static singletons, or dependency injection.

The game itself is a basic _Flappy Bird_ clone:

![GIF of Flappy Clone in action](https://user-images.githubusercontent.com/8388785/133547844-c4fcb5ca-6642-4934-9199-1de3305d1a5a.gif)

## License

The files in this repo are released under the MIT license. See [LICENSE](./LICENSE).

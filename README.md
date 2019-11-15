
# DMX on Unity

Example project that use DMX and ArtNet and demonstrate how to use it on Unity.

## Components

### Core
* #### DMX Universe

	A component which represents a DMX universe and contains some DMX devices and can have a DMX input and a DMX output.

* #### DMX Device

	An abstract component which represents a DMX device.
Note : *All your custom DMX device need to inherit from this component.*

* #### DMX Input

	An abstract Component which receives DMX data.
Note : *All your custom DMX input need to inherit from this component.*

* #### DMX Output

	An abstract Component which sends DMX data.
Note : *All your custom DMX output need to inherit from this component.*

### Custom
* #### Generic RGB device

	A DMX device which sets Unity light color. ***(Red, Green, Blue)***

* #### DMX ArtNet Input

	A component which receives DMX data by ArtNet protocol.

* #### DMX Output

	A component which sends DMX data by ArtNet protocol.

## Authors

* **Adrien Gannerie** - [GitHub](https://github.com/AdrienGannerie) - [Malt](https://www.malt.fr/profile/adriengannerie?q=Adrien%20Gannerie&as=t&searchid=5dce7d3e767eb30009cea73f)  - [Linkedin](https://www.linkedin.com/in/adrien-gannerie/)

## External Libraries

* **ArtNet Library** - [ArtNet.Net](https://github.com/MikeCodesDotNET/ArtNet.Net)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

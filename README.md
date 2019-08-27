# ManiaPlanetSharp
A .NET library, written in C#, which provides easy-to-use, object-oriented ways to access various tools and services related to ManiaPlanet.

## License
All of the code in this repository is available under the terms of the [MIT License](https://tldrlegal.com/license/mit-license).

## GameBox file parsing
Status: Work in progress
This library includes one of the most - if not the most - extensive gbx file parsers available. There is a general framework for parsing gbx files and implementations for the parsing of over 100 different chunks present in them. The full structure of data nodes can be obtained via the `GameBoxFileParser.Parse()` method. For easy access to the most commonly needed metadata, there exist special `MetadataProvider` classes which take a `GameBoxFile` and provide direct access to the metadata - no need to dig through data nodes. Furthermore, these metadata provider classes have multiple sources for most of the properties provided, so even if there are some chunks missing in the gbx file, it can mostly still provide all of the metadata properties provided. If the specific property is present multiple times in the file, the metadata provider will try to return the metadata extracted from the hardest to manipulate part of the file, where it is present.

### GameBox Todo
- [ ] Research and implement more chunks, especially regarding items and replays
- [ ] Implement parsing of ghost data
- [ ] Implement support for multiple ghosts in a single replay file
- [ ] Implement specific support for `.Shape.Gbx`, `.Mesh.Gbx` and `.Mat.Gbx` files
- [ ] Implement specific support for packs
- [ ] Implement gbx file modification
- [ ] Implement specific map modification

## ManiaPlanet text format parser
Status: Planned in the near future

## [ManiaExchange API](https://api.mania-exchange.com/documents/reference)
Status: Implemented, missing new features

## NadeoImporter
Status: Private prototype exists and works, needs proper implementation for MP#

## Dedimania API
Status: Planned

## ManiaPlanet Client Telemetry
Status: Planned
The ManiaPlanet client has a telemetry api which provides access to realtime metadata about races.

## [ManiaPlanet Web Services](https://forum.maniaplanet.com/viewforum.php?f=282)
Status: Not planned currently

## Dedicated server XML-RPC interaction
Status: Not planned currently

## ManiaCalendar API
Status: Not planned currently

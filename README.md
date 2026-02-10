# WWise-Audio-Tools

Tools to help extract WWise-based audio from various video-games.

As of February 2026, this repo is **ARCHIVED**, in favour of my [new WPF version](https://github.com/Eleiyas/WWise-Audio-Tools--WPF-).

## Details

This is a program I have admittedly gate-kept for several years now, which has allowed me to extract and upload the vast majority of the audio on several big-name "Anime Gacha Game" wikis (if you know, you know).

After [Razmoth](https://github.com/Razmoth) published "RazAudio", there really is no longer a reason for me to keep this to myself, especially given how clunky and unintuitive this program will initially feel.

Although it has a focus on extracting audio from several of the big "Anime Gacha Games", it should theoretically work for anything that uses WWise, whether Unity-based, or Unreal-based, although I will not guarantee this.

## Features

### WWise Audio Extractor

The main part of the program that allows extraction from WWise-based **PCK** and **BNK** files, although BNK extraction is fairly jank and might throw some errors, or simply break.
The program can either extract as-is, or use an input **known_filenames** [TSV](https://en.wikipedia.org/wiki/Tab-separated_values) file to automatically rename and sort files upon extract.

During initial **.WEM** extraction, the program will also generate a list of [MD5](https://en.wikipedia.org/wiki/MD5) checksums, in [CSV](https://en.wikipedia.org/wiki/Comma-separated_values) format, which allows for version-diffing. After extraction, it will likewise generate a **.txt** list of extracted files. Both of these combined allow the program to skip re-extracting files that haven't actually changed during future runs.

For obvious reasons, I will not provide any of the library files or data this program would like to use, outside of blank copies of files the program needs to actually run.

### FNV Hasher

Essentially a mini-bruteforcer that will take an input filename, and output the [FNV](https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function) hash of it.
Is compatible with FNV-32 and FNV-64, and allows for "legacy mode" for decimal output.

Example input "test":

* FNV-32: bc2c0be9
* FNV-32 (Legacy): 3157003241
* FNV-64: 8c093f7e9fccbf69
* FNV-64 (Legacy): 10090666253179731817

Depending on whether or not the library **known_hashes.txt** and **target_hashes.txt** have data, the program can also say whether or not the hashes are already known, which is useful for detecting missing files.

### Voice-Items Collator

Will find and collate filenames from a specific set of files, for a specific "Anime Gacha Game". It's incredibly specific for my own use-cases.

### FNV FileList Generator

Will take an input **.txt** list of filenames and generate the hashes for every single one, outputting a usable **known_filenames.tsv** for the main extraction program.

## Credits

Although the vast majority of the code comes from myself, or Discord buddies who have helped me, a special mention must go to [Dvingerh](https://github.com/dvingerh), as I did use their [Genshin Audio Extractor](https://github.com/dvingerh/genshin-audio-exporter) as a baseline for the [WinForms](https://en.wikipedia.org/wiki/Windows_Forms)-based nature of my own program.

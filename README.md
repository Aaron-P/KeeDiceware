To use custom word lists, change the file extension of the files depending on the format as described below and move the files into the same folder as KeePass.exe, e.g:

    C:\Program Files (x86)\KeePass Password Safe 2

KeeDiceware supports two formats of custom word lists:

1) A raw list of words; one word per line. To use a raw word list change the file extension to .wordlist and place it in the main folder with KeePass.exe. Any non-blank line will be used, so any comments or the like will need to be removed.

2) A diceware style word list; one word per line with the dice value combination in front of the word. In diceware style word list files any line that doesn't start with a numeric combination of at least 5 digits will be ignored, so comments and other lines don't need to be manually removed. To use a diceware style word list change the file extension to .diceware and place it in the main folder with KeePass.exe.

# splitchat
An IRC-style chat application for multi-threaded conversations

## Commands
SplitChat recognizes the following commands:

*Note: Parameters in {curly braces} are optional, parameters in [brackets] are required*

```
/split {topic}
	Splits the current chat window into a separate chat window

/switch [id]			
	Switches the focus from the current chat window to a different one

/merge [id] [id]	
	Merges two chat windows

/topic			
	Sets the topic for the current chat window

/close
	Closes the current chat window

/dump
	Dumps the contents of the current chat window to a text file
```

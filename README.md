Quirky, the QRKey creator
======
NOTE: Quirky is a work in progress, with more features to come.

Here's how Quirky works right now:

Put your keys in a text file with a format that's something like this:
~~~
aaaaa
bbbbb
ccccc
~~~
Add an XML file for rendering the image, in a format like this:
~~~
<RENDER HEIGHT="200" WIDTH="200">	<!--> These are the image width and height <-->
	<IMAGE FILE="test.png" X="50" Y="50" />	<!--> X and Y are position coordinates <-->
	<TEXT X="50" Y="50">Meow $KEY</TEXT>	<!--> $KEY is the key placeholder,
		which will be replaced with keys from the txt <-->
</RENDER>	<!--> Begin and end files with the "render" tag* <-->
~~~

Open the keys file, then the XML file.

An example image will be automatically generated, but you can also generate one with the "Generate" action.

Go to Actions -> Execute, and your folder will then be filled with image files generated from your keys, in the style set by the XML!

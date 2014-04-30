ServerNetworking
================
<br />
A simple framework for creating tcp servers in C#. It uses TCPListener, TCPClient and protobuf net for serialization.
<br /><br />
The whole framework is built with the Command Pattern in mind, which means that you are able to nest message responses functionality which get executed by the means of an interface, effectively reducing the amount of programming you have to do as a user, you create an object that has your server's functionality and it gets executed for each client connected to your server, you do not have to worry about multithreading, methods are provided that allow you to multicast and share data between threads, as well as attach your logic to an observer that broadcasts the server status.
<br /><br />
I have pretty much done this in 1 day and while it is a work in progress, a simple example has been provided and it can be used as it is with out any problems, maybe some extra exception handling.
<br /><br />
The packet protocol:
[OpCode][Size][data]  where OpCode is an int which has as maximum 4 bytes length, size is the length of the data which has as maximum 4 bytes length, data obviously has to be <= int32 size. Data can be encrypted, and serialuzed using protobuf or sent as raw.
<br /><br /><br /><br />

TODO
****
<br />
-Encryption, provide some basic encryption, we do not want to add extra overhead to our packets unless authentication or critical information are to be transfered.<br /><br />
-Authentication protocol, Secure Remote Password protocol (SRP 6), this is a top priority, integrating this protocol in the framework and giving an example on how to use it.<br /><br />
-Authentication, provide a ready to use authentication solution. It is worth mentioning that authentication could always be implemented by the user.<br /><br />
-More exception handling!!!<br /><br />
-Unit testing mostly for the purpose of regression.<br /><br />
-C (c99 or c11) clientside library & example for this framework, compatible with berkeley sockets and winsocks.<br />
<br />
<br /><br /><br /><br />

MANUAL
******
<br />
Create 4 new projects for each folder using the existing files. Compile for NET 4.5 (should be compatible with earlier versions too).
<br /><br /><br /><br />

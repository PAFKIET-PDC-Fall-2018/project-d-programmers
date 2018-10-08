
# Remotely Controlled Advertisement Unit

 Members
 
  -  Arbaz Ahmed (59008)
  -  Muhammad Ammar(59051)
  -  Mohammad Hamza (58998)
  -  Syed Monis Azhar (59485)  
  
  [Proposal Document](https://github.com/PAFKIET-PDC-Fall-2018/project-d-programmers/blob/master/Proposal/Project%20proposal.docx)
  
### Abstract:

This project is a remote desktop app. Client and server correspondence is at the introduce of organization provisioning over the Internet. Client sends request to multiple servers with the help of middle ware. Middle ware control multiple servers dynamically and servers usually wait to accept invitation from client side with the help of middle ware, maintaining connectivity or handshaking. Client connect to servers and provide ads list to play on servers. Sockets is introduce for basic communication between client and servers. Clients can run ads priority base remotely on servers.

### Introduction:

Server and client side both are code concern with middle ware. If servers are more than two, it&#39;s not a problem. Because they are connect with middle ware. Middle ware can easily manage multiple servers. Middle ware send services of both ends client and server. Client remotely controlled servers. Client and server process exchange information or data. Sockets we had already discussed which is a great for execute the client and server inter process communication while concealing lower layer usage subtle elements from the higher layer conceptual tasks.  Also we are use TCP Listener for connectivity. We will make windows form application on  either C# or C

### Working:

There will be a middle ware which will help to communicate with each server.

Each server will subscribe to the master server(middle ware) and client will get notified of the availability of the new server then client can connect to that server. Client will have a list of all connected servers from there it can query a single server or multiple server (broadcast). Client will query the middle ware with the destination address of destination server and after that middle ware will take care of that.

The client will ask master server to upload files to the destination server and client will also provide playlist having information of tracks, duration, priority and count of an add to be played by the server. Client can add/remove videos from desired server and can also change the playlist. All servers will be console app based.

### Language:

  For this project we will work on either C  or C# language.

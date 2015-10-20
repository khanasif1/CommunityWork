#include <SPI.h>
#include <Ethernet.h>

// Enter a MAC address for your controller below.
// Newer Ethernet shields have a MAC address printed on a sticker on the shield
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
// if you don't want to use DNS (and reduce your sketch size)
// use the numeric IP instead of the name for the server:
//IPAddress server(74,125,232,128);  // numeric IP for Google (no DNS)
char server[] = "robochimera.azurewebsites.net";    // name address for Google (using DNS)

// Set the static IP address to use if the DHCP fails to assign
IPAddress ip(10, 1, 0, 177);

// Initialize the Ethernet client library
// with the IP address and port of the server
// that you want to connect to (port 80 is default for HTTP):
EthernetClient client;

void setup() {
  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for Leonardo only
  }

  // start the Ethernet connection:
  if (Ethernet.begin(mac) == 0) {
    Serial.println("Failed to configure Ethernet using DHCP");
    // no point in carrying on, so do nothing forevermore:
    // try to congifure using IP address instead of DHCP:
    Ethernet.begin(mac, ip);
  }
  // give the Ethernet shield a second to initialize:
  delay(1000);
  Serial.println("Ethernet Connected...");
  
}

void loop()
{
  // Serial.println("Connection Call "+ String(client.connect(server, 80)));
  
  int clientConnectStatus = client.connect(server, 80);
  Serial.println("Connection Call "+ String(clientConnectStatus));
  // if you get a connection, report back via serial:
  if (clientConnectStatus == 1) {
    Serial.println("HTTP Client Connected");
     //Make a HTTP request:
    client.println("GET /api/values/GREATASIF HTTP/1.1");
    client.println("Host: robochimera.azurewebsites.net");
    //client.println("Connection: close");
    client.println();
 
  }
  else {
    // kf you didn't get a connection to the server:
    Serial.println("HTTP Client Connected - "+ String(clientConnectStatus));
  }
   client.stop();
   
  
  /*// if there are incoming bytes available
  // from the server, read them and print them:
  if (client.available()) {
    char c = client.read();
    Serial.print(c);
  }
  client.flush();*/
  
  /*// if the server's disconnected, stop the client:
  if (!client.connected()) {
  //    Serial.println();
    Serial.println("HTTP Client Disconnecting.");
    
    client.stop();

  // do nothing forevermore:
    while (true);
  }*/
   //delay(5000);
}


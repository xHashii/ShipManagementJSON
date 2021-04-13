import sys
import requests
import webbrowser
from bs4 import BeautifulSoup
import mysql.connector as mysql

DB_NAME = "shipmngt"
table_data = "data"
db = mysql.connect(
    host = "localhost",
    user = "root",
    passwd = "shipmng"
)

portNumber = input()
try:
    val = int(portNumber)
except ValueError:
    sys.exit("Not a port number.")

URL = 'https://www.marinetraffic.com/bg/ais/details/ports/' + portNumber
agent = {"User-Agent": 'Mozilla/5.0'}
page = requests.get(URL, headers=agent).text
source = str(page)

soup = BeautifulSoup(source, 'html.parser')
try:
    portName = soup.find('h1', class_='font-220 no-margin').text
except AttributeError:
    sys.exit("Not a port number.")

country = soup.find('span', class_='font-120').text
shipsOnThePortCount = soup.find('div', class_='bg-info bg-light padding-10 radius-4 text-left').text
shipURL = 'https://www.marinetraffic.com/bg/reports?asset_type=vessels&columns=flag,shipname,photo,recognized_next_port,reported_eta,reported_destination,current_port,imo,ship_type,show_on_live_map,time_of_latest_position,lat_of_latest_position,lon_of_latest_position,notes&current_port_in|begins|' + \
          portName.split(' ')[0] + "|current_port_in=" + portNumber
payload = {}
headers = {
    'User-Agent': 'Mozilla/5.0',
    'Accept': '*/*',
    'Accept-Language': 'en-US,en;q=0.5',
    'Vessel-Image': '000000000000000000000000000000000000',
    'X-Requested-With': 'XMLHttpRequest',
    'Connection': 'keep-alive',
}
response = requests.request("GET", shipURL, headers=headers, data=payload)
d = response.json()
for ship in d['data']:
    shipName = ship['SHIPNAME']
    shipIMO = ship["IMO"]
    shipType = ship['TYPE_SUMMARY']

ptN = str(portName).replace(portName.split(' ')[len(portName.split(' ')) - 1], '')
ptC = country.replace(country.split(' ')[0], '')
coords = shipsOnThePortCount.replace('\n', '~').split('~')[3]
SoP = int(shipsOnThePortCount.replace('\n', '~').split('~')[18])
unlC = shipsOnThePortCount.replace('\n', '~').split('~')[14]

cursor = db.cursor()
cursor.execute("SHOW DATABASES")

if DB_NAME in cursor:
    cursor.fetchone()[0]==1
    print(f"{DB_NAME} found.")
else:
    print(f"Database {DB_NAME} doesn't exist, creating now....")
    cursor.execute(f"CREATE DATABASE IF NOT EXISTS {DB_NAME}")
    print(f"Database {DB_NAME} created!")
db.cmd_init_db(DB_NAME)
if table_data in cursor:
    print()
else:
    cursor.execute("CREATE TABLE IF NOT EXISTS data(PortName VARCHAR(12) NOT NULL,PortNumber INTEGER NOT NULL,Country VARCHAR(20) NOT NULL,Coordinates VARCHAR(24) NOT NULL,ShipsOnPort INTEGER NOT NULL,Unlocode VARCHAR(5) NOT NULL,ShipName VARCHAR(14) NOT NULL,ShipType VARCHAR(13) NOT NULL,IMO INTEGER NOT NULL);")

query = "INSERT INTO Data(PortName, PortNumber, Country, Coordinates, ShipsOnPort, Unlocode, ShipName, ShipType, IMO) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s)"
values = (ptN, portNumber, ptC, coords, SoP, unlC, shipName, shipType, shipIMO)
cursor.execute(query, values)
db.commit()
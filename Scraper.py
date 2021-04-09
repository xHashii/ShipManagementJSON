import json
from tkinter import Frame

import requests
import webbrowser
from bs4 import BeautifulSoup
import datetime
import sys

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




class storeData(Frame):
    Data = {'Data': [{
    "PortName": portName.split(' ')[0],
    "PortNumber": portNumber,
    "Country": country.split(' ')[1],
    "Coordinates": shipsOnThePortCount.replace('\n', '~').split('~')[3],
    "ShipsOnPort": int(shipsOnThePortCount.replace('\n', '~').split('~')[18]),
    "Un/locode": shipsOnThePortCount.replace('\n', '~').split('~')[14]
}]}



    with open('pD.json', encoding='utf-8') as outfile_r:
        try:
            source_data = json.load(outfile_r)
            new_data = True
            
            source_data['Data'].append({'PortName': portName.split(' ')[0], 'PortNumber': portNumber, 'Country': country.split(' ')[1], 'Coordinates': (str(shipsOnThePortCount).replace('\n', '~')).split('~')[3], 'ShipsOnPort': int(shipsOnThePortCount.replace('\n', '~').split('~')[18]), 'Un/locode':shipsOnThePortCount.replace('\n', '~').split('~')[14]})
            storage = source_data
        except:
            storage = Data
    with open("pD.json", "w", encoding='utf-8') as outfile_w:
        json.dump(storage, outfile_w, indent=2, ensure_ascii=False)

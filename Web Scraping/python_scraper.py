#!/usr/bin/env python

import requests
import bs4
import csv


def find_between( text, first, last ):
    try:
        start = text.index( first ) + len( first )
        end = text.index( last, start )
        return text[start:end]
    except ValueError:
        return ""

def extract_name(str_val):
    return find_between( str_val, '>', '<' )


def extract_id(url_text):
    return int(url_text[url_text.index('y')+2:])


def calc_num_pages(whole_soup):
    sub_soup = whole_soup.findAll('td', attrs={'width':'*'})
    for crumb in sub_soup:
        if('Page 1' in crumb.text):
            return int(find_between(crumb.text, 'Page 1 of ', ','))


def write_sub_dict_csv(sub_dict, id0):
    word = csv.writer(open("storage/zoo_"+str(id0)+".csv", "w"))
    for key, val in sub_dict.items():
        word.writerow([key, val])

def process_webpage(url_text):
    response = requests.get(url_text)
    while(response.status_code != 200):
        response = requests.get(url_text)
        print ' ******* Problem getting webpage ', url_text
        print ' ******* Tried again '
    return bs4.BeautifulSoup(response.text)


def extract_table_links(table_dict, sub_soup):
    subtext = sub_soup.find('table', attrs={'cellpadding':'0'})
    for link in subtext.find_all('a'):
        href = link.get('href')
        if(not(href == None)  and  'ntaxa' in href):
            table_dict[href] = link
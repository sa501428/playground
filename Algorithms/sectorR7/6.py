#!/usr/bin/python

import math

n = 5
m = math.factorial(n)
print m

b = set()

class Node(object):
	def __init__(self, parentstring, data):
		self.data = data
		self.accum = parentstring
		self.children = []

	def process(self):
		if (len(self.data) < 1):
			b.add(self.accum)
		else:	
			for j in self.data:
				scopy = list(self.data)
				scopy.remove(j)
				child = Node(self.accum+str(j), scopy)
				child.process()
				self.add_child(child)


	def add_child(self, obj):
		self.children.append(obj)




a = Node("",range(1,n+1))
a.process()

for j in b:
	j2 = str(j)
	news = ""
	for k in j2:
		news += k + " "
	print news[:-1]
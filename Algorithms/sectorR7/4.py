#!/usr/bin/python


#z = 'CTCGTCATTCTTAGGAGAAGTACGCGAGTTCTTAGACAGTTTTAGCGCCCTTGGGCTGTGAGCTTTTCTACTGAAATGACCTCGGGCTAAGTGTGCTAGTTTAGTTGTTCACGTGAAGCGACAATAACTGATTCCCAGACGCCCCCACTCCACCATCTGATCACCCTCATTTTCAAGCTCCAATTCCTGCTGCGGTGCGTCTATCTTTGTACAGAAGCAACGGTTCTCTGAGGATTACTTCACAAAGTCGTAACTTTTCTCTCTAACCACCGTCGTTGGAACGAATGACGGTTGAATATTCGTATATTGATTATTTTACCCTACCACCCCAGTAGCCCTCTTATGACATCCTAGGTTCTCTTTTTGGTGGCGTCCACCATCCGAATGTTATGAACTACTTTTAGGTTTTGCAAGATGATATAAATCTTGCGGCATAGGGTATTTTCCACTTCCCTAGCTGCCGACTTCCCTGCCGTCAGCTTATTATCTGTCCCAGTTACGTAATCCCTGCGCCTACTCACTTTGCAGTTGTATTCCTCTGGAGTCAACACGGTCAGTACTGTCGCCGAGCTGTGCAACTCATATGAATTTACTACGACTCGGTCTATCGTAAAAACCGGTTCCTAGATGTTGCTCATAAATTCTGTGATTTGGTGGAACTGGGGACCCCAATCCAGACGGTCACACTAGCTAACAGTCGCGTTCGGCCTTTCGCGGACCGCTACGTTTGCTACTTCAGAAATCCGCTAACACTTAAAGAATGAACCGTCACGCCTGCCGTCCATCTTAATTGACTGGAAAAAATGACGTAGCCCACCAAGAGATAGTGATAACTGACGGGTCTGTAACCCTAAACCTTGATCGTAGAACTTGTTGCTCAT'
z = 'GGGACGTATACTCCCATCCAGCGCTCCGAACGCCTACGTGCAGAAATGAAAGGCACGAGGGCTATGAGATCTACTCCTCTATCCGGCATAAATATTAGACGCGGCGATATGGGAGTTATGTAGGACGTCTATCACCTAAAATTTTTTACTGCTTGGAGATGTGGCGTTTATGACCTAACATTCGAAGGGAGGTACACCGTGTGCGGTTGTCAAAAAATCTCTCACACCAGTTTCCAGGTTGGGGTATCAAAGGGAGGGCCATCAGCGTCACTTAACATTACCGGATAGCGTGAGAAAAAGGATGGAGCGGTTAAGATGCGCGAACTTCAAAGTATACGCCTGTTTGCCACAGTCTTCCTGCGGACTGAAGGTCGAACTGACGTAAAGCATAACTGAAACCTTTACGGACCGCAAATTACGATTCCGTAGGATGGTTACCCGCTACCGATCTGTCCGTTTGAGCAATAAGGGACTTGGTGGTACAATAAGTGCTTCGCGAGGATGAATTAGATTTGGATGAAAGGCCTTTCGCCTGGTCGGGATGGTATCATGACCTGAGGTCAGCTGGGATTACTTGAGACAGGTCGTCGACAGGATAGTTAAAGCTCTCGTCTTTCAAAGTTACGATCTAAATGCAATGTGCGTTCAGATTAAAAGGGCTAGATCTGCGGATCTCGCAAAAGCTCACGCATAGCATCATGCCATATCGAAGCTTAAGCGGGCCACAGTGATAGCTGGTCAGAAGACTAGGTACACAGTGGTCAGTGACTTAGGCCCCCTTGAGGTCGTCTCCGGGGGAGGGTTTAGGGACTGAGGGGGGTTGCTGCAAGACACTCTCTTGTCG'
z = z.replace('A','1')
z = z.replace('T','2')
z = z.replace('C','3')
z = z.replace('G','4')
z = z.replace('1','T')
z = z.replace('2','A')
z = z.replace('3','G')
z = z.replace('4','C')
print z[::-1]




rowa = 1:500;
rowb = rowa;
[rowA, rowB] = meshgrid(rowa, rowb);


rowa2 = rowa.^2;
rowb2 = rowa2;
[rowA2, rowB2] = meshgrid(rowa2, rowb2);

rowC2 = rowA2 + rowB2;

rowC = sqrt(rowC2);

total = rowC + rowA + rowB;

[x,y] = find(total == 1000);

a = x(1);
b = y(1);

c = sqrt(a^2 + b^2);
disp(a*b*c)







n = 10001;

n2 = 4;

p = primes(2^n2);
while(length(p) < n)
    n2 = n2+1;
    p = primes(2^n2);
end

disp(p(n))


% btw quick answer is  max(factor(n))

n = 600851475143;

valid_primes = primes(sqrt(n));

factor_indx = 1;

max_lim = sqrt(n);

while (n > 1)
    if(valid_primes(factor_indx) > max_lim)
        % n is prime
        valid_primes(factor_indx) = n;
        valid_primes = valid_primes(1:factor_indx);
        n = 1;
    elseif(mod(n, valid_primes(factor_indx)) == 0)
        
        while(mod(n, valid_primes(factor_indx)) == 0)
            n = n / valid_primes(factor_indx);
        end
        
        max_lim = sqrt(n);
        factor_indx = factor_indx+1;
        
        
    else
        valid_primes(factor_indx) = 0;
        factor_indx = factor_indx+1;
    end
    
    
end

valid_primes = valid_primes(valid_primes > 1);

max(valid_primes)

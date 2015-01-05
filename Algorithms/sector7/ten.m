
% cheat
%disp(sum(prime(2000000)));


n = 2e6;

prime_candidates = 1:n;
prime_candidates(1) = 0;

offset = 2;

indx = 2;

while(true)
   % val at indx is prime
   % eliminate all higher multiples
   
   prime_candidates(2*indx:indx:end) = 0;
   if(indx+offset > n)
       break;
   end
   delta_indx = find(prime_candidates(indx+1:indx+offset)>0,1);
   if(isempty(delta_indx) && offset > 1e6)
       break
   elseif isempty(delta_indx)
       offset = offset * 2;
   else
       indx = indx+delta_indx;
   end
end

disp(sum(prime_candidates))
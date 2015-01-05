
function four

a = 100:999;
all_mult = triu(a'*a);
all_mult = all_mult(:);
all_mult = all_mult(all_mult > 0);
all_mult = flip(sort(all_mult));

indx = 1;
not_done = true;

while(not_done)
    if(is_palindrome(all_mult(indx)))
        not_done = false;
    else
        indx = indx + 1;
    end
end

disp(all_mult(indx))

return

function status = is_palindrome(n)
val = num2str(n);
status = strcmp(val,flip(val));
return
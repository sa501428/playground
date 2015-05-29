
%
% Script for cartoonifying a given image
%

function cartoonify(filename)

img = imread(filename);

img = categorize(img);
fshow(img)

imgEdges = sobelify(img);
fshow(imgEdges)

finalImg = edgeMerger(imgEdges,img);
fshow(finalImg)

end

function A = edgeMerger(Edg,A)

m = 200;
Edg(Edg > m) = 255;
Edg(Edg <= m) = 0;

B = (255-Edg)/255;
A(:,:,1) = A(:,:,1).*B;
A(:,:,2) = A(:,:,2).*B;
A(:,:,3) = A(:,:,3).*B;

end

function B = sobelify(A)

h2 = fspecial('sobel');
gy = double(imfilter(A, h2));
gx = double(imfilter(A, h2'));
gy2 = double(imfilter(A, -h2));
gx2 = double(imfilter(A, -h2'));
B = double((gy.*gy + gx.*gx + gy2.*gy2 + gx2.*gx2)/2).^(0.5);
B = uint8(B);
B = rgb2gray(B);

end

function A = gaussify(A, n, s)
h = fspecial('gaussian', n, s);
A = imfilter(A,h);
end

function A = posterize(A, n)
A = A/n * n;
end

function A = posterizev2(A, n)
m = 255.0/n;
A = uint8(round(double(A-1)/m)*m);
end

function A = kwuhara(A)

intens = rgb2gray(A);

[s1, s2, ~] = size(A);

r1 = [0 1 1; 0 1 1; 0 0 0];
r2 = [1 1 0; 1 1 0; 0 0 0];
r3 = [0 0 0; 1 1 0; 1 1 0];
r4 = [0 0 0; 0 1 1; 0 1 1];
rs = {r1, r2, r3, r4};
ms = cell(1,4);
ss = zeros(s1,s2,4);
for i=1:4
    ms{i} = imfilter(A, rs{i}/4);
    ss(:,:,i) = stdfilt(intens, rs{i});
end

[~,ssmin] = min(ss,[],3);
for i = 2:s1-1
    for j = 2:s2-1
        A(i,j,:) = ms{ssmin(i,j)}(i,j,:);
    end
end

end

function fshow(A)
figure
imshow(A)
end

function A = categorize(A)

colors =   [0, 0, 0; % Black
            101, 67, 33; % dbrown
            148, 63, 7; % Brown
            230, 215, 190; % sand
            250, 250, 250; % White
            9, 197, 244; % Sky Blue
            40, 98, 185; % Blue
            201, 17, 17; % Red
            216, 78, 9;  % Red Orange
            255, 128, 0; % Orange
            246, 235, 32; % Yellow
            81, 194, 1; % Yellow Green
            28, 142, 13; % Green
            126, 68, 188; % Violet
            204, 132, 84; % Tan
            %248, 99, 203; % Magenta
            %194, 178, 128; % sand
            252, 168, 204; % Pink
            80, 80, 80; % dgray
            160, 160, 160]; % lgray
l = length(colors);
A = double(A);
[imax, jmax, kmax] = size(A);
for i = 1:imax
    for j = 1:jmax
        c = reshape(A(i,j,:),[1 3]);
        c = repmat(c,l,1);
        c = c-colors;
        c = abs(c);
        c = c(:,1) + c(:,2) + c(:,3);
        [~,index] = min(c);
        A(i,j,:) = reshape(colors(index,:),[1 1 3]);
        for k = 1:length(c)
            if(c(k) < .4*255)
                A(i,j,:) = reshape(colors(k,:),[1 1 3]);
                break;
            end
        end
    end
end
A = uint8(A);

end
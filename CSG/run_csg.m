
%
% run_csg.m
%
% Muhammad S Shamim
%
% Matlab code to generate color space gradient transformations
% on an existing image
%
% usage example
% run_csg('sunset.jpg')
%

function run_csg(input_file_name)

% read image
image = imread(input_file_name);
image = double(image);

% generate color space transformation
% in this case     RGB --> GBR --> BGR --> BRG --> RGR
csg_transforms = {[1,2,3],[2,3,1],[3,2,1],[3,1,2],[1,2,1]};
image = apply_transforms(image, csg_transforms);

% save to new file
imwrite(uint8(image),strcat('csg_',input_file_name));

return

%
% Split image into equal sections for the transformations
%

function image = apply_transforms(image, transform_cell)

num_transforms = length(transform_cell);

jmax = size(image,2);
indices = ceil(linspace(1,jmax,num_transforms));
offset = 0;

for i = 1:(num_transforms-1)
    image(:,(offset+indices(i)):indices(i+1),:) =...
        sub_transform(image(:,(offset+indices(i)):indices(i+1),:),...
        transform_cell{i}, transform_cell{i+1});
    offset = 1;
end

return


function image_section = sub_transform(image_section,...
    transform_0, transform_1)

[imax, jmax, kmax] = size(image_section);

transforms = zeros(3,3,jmax);
alphas = linspace(1,0,jmax);
betas = 1-alphas;

% section transformation gradient matrices
for i = 1:3
    transforms(i,transform_0(i),:) = reshape(transforms(i,transform_0(i),:),1,jmax) + alphas;
    transforms(i,transform_1(i),:) = reshape(transforms(i,transform_1(i),:),1,jmax) + betas;
end

for j = 1:jmax
    current_transform = transforms(:,:,j);
    for i = 1:imax
        image_section(i,j,:) = current_transform*reshape(image_section(i,j,:),kmax,1);
    end
end

return
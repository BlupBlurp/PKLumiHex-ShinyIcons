import os
import re

import sys

# Get-ChildItem "PKHeX.Drawing.PokeSprite/Resources/img/shiny_icons" -Filter *.png | ForEach-Object { magick $_.FullName -resize 56x56^ -gravity center -background none -extent 68x56 $_.FullName }

base_dir = os.path.dirname(os.path.abspath(__file__))
shiny_icons_dir = os.path.join(
    base_dir, "PKHeX.Drawing.PokeSprite", "Resources", "img", "shiny_icons"
)
big_shiny_sprites_dir = os.path.join(
    base_dir, "PKHeX.Drawing.PokeSprite", "Resources", "img", "Big Shiny Sprites"
)

# Get all files in the directories
shiny_icons_files = sorted(os.listdir(shiny_icons_dir))
big_shiny_sprites_files = sorted(os.listdir(big_shiny_sprites_dir))

# Create a mapping of Big Shiny Sprites names to their first number
big_shiny_mapping = {}
for file in big_shiny_sprites_files:
    if "gmaxs" in file:
        continue  # Skip gmaxs files
    match = re.match(r"b_(\d+)(?:-(\d+))?s\.png", file)
    if match:
        number = match.group(1)
        sub_number = match.group(2) or "0"
        key = f"{int(number):04d}_{int(sub_number):02d}"
        big_shiny_mapping[key] = file

# Rename files in shiny_icons
from collections import defaultdict

# Collect all variants for each key
icon_variants = defaultdict(list)
for file in shiny_icons_files:
    match = re.match(r"pm(\d{4})_(\d{2})_(\d{2})\.png", file)
    if match:
        base_number = match.group(1)
        sub_number = match.group(2)
        variant = match.group(3)
        key = f"{base_number}_{sub_number}"
        icon_variants[key].append((variant, file))

renamed_files = set()
for key, variants in icon_variants.items():
    if key not in big_shiny_mapping:
        continue
    # Prefer variant '01', otherwise use the only variant
    chosen = None
    for variant, file in variants:
        if variant == "01":
            chosen = file
            break
    if not chosen and len(variants) == 1:
        chosen = variants[0][1]
    if chosen:
        new_name = big_shiny_mapping[key]
        src = os.path.join(shiny_icons_dir, chosen)
        dst = os.path.join(shiny_icons_dir, new_name)
        print(f"Renaming {src} -> {dst}")
        if os.path.exists(dst):
            print(f"Skipped: {dst} already exists.")
        else:
            os.rename(src, dst)
            renamed_files.add(key)
            print(f"Renamed: {chosen} -> {new_name}")
if not renamed_files:
    print("No files were renamed.")

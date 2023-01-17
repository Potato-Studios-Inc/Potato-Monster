#!/bin/bash

# This script creates a self-extracting installer for the Mac OS app using the create-dmg brew package.

# Usage: ./create-installer.sh

# Install the create-dmg package if it is not already installed
if ! brew ls --versions create-dmg > /dev/null; then
  brew install create-dmg
fi

# Create the installer
create-dmg \
  --volname "Potato Monster" \
  --volicon "Potato Monster.app/Contents/Resources/PlayerIcon.icns" \
  --window-pos 200 120 \
  --window-size 800 400 \
  --icon-size 100 \
  --icon "Potato Monster.app" 200 190 \
  --hide-extension "Potato Monster.app" \
  --app-drop-link 600 185 \
  "Potato Monster Installer.dmg" \
  "Potato Monster.app"

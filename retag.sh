#!/bin/bash

# Ensure two arguments are provided (OLD_TAG and NEW_TAG).
if [ $# -lt 2 ]; then
  echo "Error: Missing arguments."
  echo "Usage: $0 <OLD_TAG> <NEW_TAG>"
  echo "Example: ./retag.sh v1.1.0 v1.1.0" to replace the tag!
  exit 1
fi

OLD_TAG="$1"
NEW_TAG="$2"

echo "Removing old local tag (if exists): $OLD_TAG"
git tag -d "$OLD_TAG" 2>/dev/null

echo "Removing old remote tag (if exists): $OLD_TAG"
git push origin ":refs/tags/$OLD_TAG" 2>/dev/null

echo "Creating new local tag: $NEW_TAG"
git tag "$NEW_TAG"

echo "Pushing new tag to remote: $NEW_TAG"
git push origin "$NEW_TAG"

echo "Done! '$OLD_TAG' (if it existed) was removed, and '$NEW_TAG' has been created and pushed."
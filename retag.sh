#!/bin/bash

TAG="${1:-v1.0.0}"

echo "Removing local tag: $TAG"
git tag -d "$TAG" 2>/dev/null

echo "Removing remote tag: $TAG"
git push origin ":refs/tags/$TAG"

echo "Creating local tag: $TAG"
git tag "$TAG"

echo "Pushing tag: $TAG"
git push origin "$TAG"

echo "Done! The tag '$TAG' has been recreated and pushed."

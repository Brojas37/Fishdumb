#!/bin/bash
echo "Uploading IPA to Appstore Connect..."
#Path is "/BUILD_PATH/<ORG_ID>.<PROJECT_ID>.<BUILD_TARGET_ID>/.build/last/<BUILD_TARGET_ID>/build.ipa"
path="$WORKSPACE/.build/last/$TARGET_NAME/build.ipa"
echo "WORKSPACE: $WORKSPACE"
echo "TARGET_NAME: $TARGET_NAME"
echo "IPA Path: $path"

xcodeproj_path="$WORKSPACE/Unity-iPhone.xcodeproj/project.pbxproj"
if grep -q "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES" "$xcodeproj_path"; then
    sed -i '' 's/ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES = YES;/ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES = NO;/g' "$xcodeproj_path"
fi

if xcrun altool --upload-app -t ios -f $path -u $ITUNES_USERNAME -p $ITUNES_PASSWORD ; then
    echo "Upload IPA to Appstore Connect finished with success"
else
    echo "Upload IPA to Appstore Connect failed"
fi

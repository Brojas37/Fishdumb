#!/bin/bash
echo "Uploading IPA to Appstore Connect..."
#Path is "/BUILD_PATH/<ORG_ID>.<PROJECT_ID>.<BUILD_TARGET_ID>/.build/last/<BUILD_TARGET_ID>/build.ipa"
path="$WORKSPACE/.build/last/$TARGET_NAME/build.ipa"
echo "WORKSPACE: $WORKSPACE"
echo "TARGET_NAME: $TARGET_NAME"
echo "IPA Path: $path"

export OTHER_SWIFT_FLAGS="-Xfrontend -embed-bitcode=no"

if xcrun altool --upload-app -t ios -f $path -u $ITUNES_USERNAME -p $ITUNES_PASSWORD ; then
    echo "Upload IPA to Appstore Connect finished with success"
else
    echo "Upload IPA to Appstore Connect failed"
fi

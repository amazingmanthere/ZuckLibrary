//
//  NSPathEx.m
//  audioplayerDemo
//
//  Created by zuckchen on 2/28/15.
//  Copyright (c) 2015 zuckchen. All rights reserved.
//

#import "NSPathEx.h"

@implementation NSPathEx

+(NSString*)HomePath
{
    return NSHomeDirectory();
}

+(NSString*)TempPath
{
    NSString *path = [NSTemporaryDirectory() stringByStandardizingPath];
    return path;
}

+(NSString*)DocPath
{
    NSString* strPath =  [[NSBundle mainBundle] bundlePath];
    NSString *documentsDirectory;
    if ([strPath hasPrefix:@"/var/mobile/Applications"]) {
        NSRange range = [strPath rangeOfString:@"/" options:NSBackwardsSearch];
        strPath = [strPath substringToIndex:range.location];
        documentsDirectory = [strPath stringByAppendingFormat:@"/Documents"];
    }else {
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        documentsDirectory = [paths objectAtIndex:0];
    }
    return documentsDirectory;
    
}

+(NSString*)LibPath
{
    NSString* strPath = [[NSBundle mainBundle] bundlePath];
    NSString *libraryDirectory = nil;
    if ([strPath hasPrefix:@"/var/mobile/Applications"]) {
        NSRange range = [strPath rangeOfString:@"/" options:NSBackwardsSearch];
        strPath = [strPath substringToIndex:range.location];
        libraryDirectory = [strPath stringByAppendingFormat:@"/Library"];
    }else {
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSLibraryDirectory, NSUserDomainMask, YES);
        libraryDirectory = [paths objectAtIndex:0];
    }
    return libraryDirectory;
}

+(NSString*)AppPath
{
    
    NSString* strPath =  [[NSBundle mainBundle] bundlePath];
    return strPath;
}

+(NSString*)convertToAbsolutePath:(NSString*)path
{
    if (nil == path || ![path isKindOfClass:[NSString class]]) {
        return nil;
    }
    
    if ([path hasPrefix:@"assets-library://"]) {
        return path;
    }
    
    NSString *absolutePath = nil;
    NSString *rootPath = [[NSPathEx DocPath] stringByDeletingLastPathComponent];
    
    if ([NSPathEx pathIsAbsolute:path]) {
        NSString *directory = nil;
        if ([path rangeOfString:@"Documents/"].location != NSNotFound) {
            directory = @"Documents/";
        } else if ([path rangeOfString:@"Library/"].location != NSNotFound) {
            directory = @"Library/";
        } else {
            directory = @"/";
        }
        NSArray *segStringArray = [path componentsSeparatedByString:directory];
        absolutePath = [NSString stringWithFormat:@"%@/%@%@",rootPath,directory,[segStringArray lastObject]];
    } else {
        absolutePath = [rootPath stringByAppendingPathComponent:path];
    }
    return  absolutePath;
}

+(NSString*)convertToRelativePath:(NSString*)path
{
    if (nil == path || ![path isKindOfClass:[NSString class]]) {
        return nil;
    }
    
    NSString *relativePath = nil;
    
    if ([NSPathEx pathIsAbsolute:path]) {
        NSString *directory = nil;
        if ([path rangeOfString:@"Documents/"].location != NSNotFound) {
            directory = @"Documents/";
        } else if ([path rangeOfString:@"Library/"].location != NSNotFound) {
            directory = @"Library/";
        } else {
            directory = @"/";
        }
        NSArray *segStringArray = [path componentsSeparatedByString:directory];
        relativePath = [directory stringByAppendingPathComponent:[segStringArray lastObject]];
    } else {
        relativePath = path;
    }
    return relativePath;
}

+(BOOL)pathIsAbsolute:(NSString*)path
{
    if ([path hasPrefix:@"/private/var/mobile/Applications"])
        return YES;
    if ([path hasPrefix:@"/var/mobile/Applications"])
        return YES;
    if ([path hasPrefix:[[[NSPathEx DocPath] stringByDeletingLastPathComponent] stringByDeletingLastPathComponent]]) {
        return YES;
    }
    return NO;
}

@end

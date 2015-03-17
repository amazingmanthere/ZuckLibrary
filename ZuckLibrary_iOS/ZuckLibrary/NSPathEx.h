//
//  NSPathEx.h
//  audioplayerDemo
//
//  Created by zuckchen on 2/28/15.
//  Copyright (c) 2015 zuckchen. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NSPathEx : NSObject

+ (NSString*)HomePath;
+ (NSString*)TempPath;
+ (NSString*)DocPath;
+ (NSString*)LibPath;
+ (NSString*)AppPath;

//将文件路径转换为绝对路径
+(NSString*)convertToAbsolutePath:(NSString*)path;
//将文件路径转换为相对路径
+(NSString *) convertToRelativePath:(NSString*)path;
+(BOOL)pathIsAbsolute:(NSString*)path;

@end

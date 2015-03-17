//
//  OSInfoUtil.m
//  ZuckLibrary
//
//  Created by zuckchen on 6/10/14.
//  Copyright (c) 2014 zuckchen. All rights reserved.
//

#import "OSInfoUtil.h"
#import <UIKit/UIKit.h>

@implementation OSInfoUtil

// 获取版本号
+ (double)getOSVersionNum{
    double version = [[UIDevice currentDevice].systemVersion doubleValue];
    return version;
}

// 获取版本名称
+ (NSString*)getOSVersionName{
    return  [[UIDevice currentDevice] systemName];
}

@end

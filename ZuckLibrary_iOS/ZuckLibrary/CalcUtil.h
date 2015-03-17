//
//  CalcUtil.h
//  Calculator
//
//  Created by zuckchen on 5/15/14.
//  Copyright (c) 2014 zuckchen. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CalcUtil : NSObject

+ (double)Plus:(double)s1 secondaryDouble:(double)s2;
+ (double)Plus:(NSString*)s1 secondaryString:(NSString*)s2;

+ (double)Sub:(double)s1 secondaryDouble:(double)s2;
+ (double)Sub:(NSString*)s1 secondaryString:(NSString*)s2;

+ (double)Div:(double)s1 secondaryDouble:(double)s2;
+ (double)Div:(NSString*)s1 secondaryString:(NSString*)s2;

+ (double)Multi:(double)s1 secondaryDouble:(double)s2;
+ (double)Multi:(NSString*)s1 secondaryString:(NSString*)s2;

@end

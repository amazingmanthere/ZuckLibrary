//
//  CalcUtil.m
//  Calculator
//
//  Created by zuckchen on 5/15/14.
//  Copyright (c) 2014 zuckchen. All rights reserved.
//

#import "CalcUtil.h"

@implementation CalcUtil

+ (double)Plus:(double)s1 secondaryDouble:(double)s2{
    return s1+s2;
}

+ (double)Plus:(NSString*)s1 secondaryString:(NSString*)s2{
    double p1 = [s1 doubleValue];
    double p2 = [s2 doubleValue];
    
    return [self Plus:p1 secondaryDouble:p2];
}

+ (double)Sub:(double)s1 secondaryDouble:(double)s2{
    return  s1 - s2;
}

+ (double)Sub:(NSString*)s1 secondaryString:(NSString*)s2{
    double p1 = [s1 doubleValue];
    double p2 = [s2 doubleValue];
    
    return [self Sub:p1 secondaryDouble:p2];
}

+ (double)Div:(double)s1 secondaryDouble:(double)s2{
    return s1 / s2;
}

+ (double)Div:(NSString*)s1 secondaryString:(NSString*)s2{
    double p1 = [s1 doubleValue];
    double p2 = [s2 doubleValue];
    
    return [self Div:p1 secondaryDouble:p2];
}

+ (double)Multi:(double)s1 secondaryDouble:(double)s2{
    return  s1 * s2;
}

+ (double)Multi:(NSString*)s1 secondaryString:(NSString*)s2{
    double p1 = [s1 doubleValue];
    double p2 = [s2 doubleValue];
    
    return [self Multi:p1 secondaryDouble:p2];
}

@end

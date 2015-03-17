//
//  StringUtils.m
//  mtt
//
//  Created by puckshuang on 09-12-26.
//  Copyright 2009 tencent. All rights reserved.
//

#import <CommonCrypto/CommonDigest.h>
#import "StringUtils.h"


@implementation NSString (Function)

- (id)initWithUTF8StringSafety:(const char *)nullTerminatedCString{
    if(!nullTerminatedCString){
        return nil;
    }
    
    id ret = [self initWithUTF8String:nullTerminatedCString];
    
    return ret;
}

- (NSString*)stringByTrimmingLeadingWhitespace
{
    
    if(0 == self.length)
        return self;
    
    NSInteger i = 0;
    while ((i < [self length])
           && [[NSCharacterSet whitespaceCharacterSet] characterIsMember:[self characterAtIndex:i]])
    {
        i++;
    }
    return [self substringFromIndex:i];
}

- (NSInteger)getUnicodeByteLength{
    
    int strlength = 0;
    char* p = (char*)[self cStringUsingEncoding:NSUnicodeStringEncoding];
    for (int i=0 ; i<[self lengthOfBytesUsingEncoding:NSUnicodeStringEncoding] ;i++) {
        if (*p) {
            p++;
            strlength++;
        }
        else {
            p++;
        }
    }
    return strlength;
}

+ (NSString*)encodingUrlString
{
    NSString* urlString = (NSString*)CFBridgingRelease(CFURLCreateStringByAddingPercentEscapes(NULL, (CFStringRef)self, NULL, CFSTR("!*'();:@&=+$,/?%#[]"),kCFStringEncodingUTF8));
    return urlString; 
}

@end

@implementation StringUtils


+ (NSString*)trim:(NSString*)_str
{
	if(_str == nil)
		return _str;
	return [_str stringByTrimmingCharactersInSet:[NSCharacterSet whitespaceAndNewlineCharacterSet]];
}

+ (NSString*)MD5:(NSString*)str
{
	const char *cStr = [str UTF8String];
	
	unsigned char result[CC_MD5_DIGEST_LENGTH];
	
	CC_MD5( cStr, (unsigned int)strlen(cStr), result );
	
	return [NSString 
			
			stringWithFormat: @"%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X%02X",
			
			result[0], result[1],
			
			result[2], result[3],
			
			result[4], result[5],
			
			result[6], result[7],
			
			result[8], result[9],
			
			result[10], result[11],
			
			result[12], result[13],
			
			result[14], result[15]
			
			];
}

+ (NSString*)getRidOfCNWhitespace:(NSString*)str
{
    if(!str)
        return nil;
        
    unichar unichars[] = {(unichar)8198};
    NSString* whiteSpaceChar = [NSString stringWithCharacters:unichars length:1];
    return [str stringByReplacingOccurrencesOfString:whiteSpaceChar withString:@""];
}

+ (BOOL)isEmpty:(NSString *)str
{
    return (!str) || (0 == [str length]);
}

/**
 * 判断一个字符串是否是数字
 */
+ (BOOL)isNumeric:(NSString *)str
{
    if ([StringUtils isEmpty:str])
    {
        return NO;
    }
    
    for (int i = (int)[str length]; --i >= 0;)
    {
        unichar chr = [str characterAtIndex:i];
        if (chr < 48 || chr > 57)
            return NO;
    }
    return YES;
}

+ (BOOL)haveChineseChar:(NSString* )str
{
    if ([StringUtils isEmpty:str])
    {
        return NO;
    }
    
    int len = (int)[str length];
    
    for (int i = len - 1; i >= 0; i--)
    {
        unichar c = [str characterAtIndex:i];
        
        if ((c >= 0x4E00 && c <= 0x9FFF) || (c >= 0xFE30 && c <= 0xFFA0))
        {
            return YES;
        }
    }
    
    return NO;
}

+ (BOOL)IsURL:(NSString*)str {
    NSString *urlRegEx =
    @"(http|https)://((\\w)*|([0-9]*)|([-|_])*)+([\\./?=&]((\\w)*|([0-9]*)|([-|_])*))+";
    NSPredicate *urlTest = [NSPredicate predicateWithFormat:@"SELF MATCHES %@", urlRegEx];
    return [urlTest evaluateWithObject:str];
}

@end

#pragma once

#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

#include <iostream>
#include <map>
#include <vector>

extern int main(int argc, char *argv[]);
extern void arg(int argc, char *argv);

#define Msz 0x10000
#define Rsz 0x100
#define Dsz 0x10

#define byte uint8_t
#define cell int16_t
#define ucell uint16_t

extern byte M[Msz];
extern ucell Cp;
extern ucell Ip;
extern ucell R[Rsz];
extern ucell Rp;
extern cell D[Dsz];
extern ucell Dp;

extern void vm();

enum class cmd : byte { nop = 0x00, halt = 0xFF };

extern void bc(byte b);

extern void nop();
extern void halt();

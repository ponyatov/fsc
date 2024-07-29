#include "fsc.hpp"

int main(int argc, char *argv[]) {  //
    arg(0, argv[0]);
    bc(nop);
    bc(halt);
    vm();
}

void arg(int argc, char *argv) {  //
    fprintf(stderr, "argv[%i] = <%s>\n", argc, argv);
}

byte M[Msz];
ucell Cp = 0;
ucell Ip = 0;
ucell R[Rsz];
ucell Rp = 0;
cell D[Dsz];
ucell Dp = 0;

void vm() {
    while (true) {
        assert(Ip < Cp);
        uint8_t op = M[Ip++];
        fprintf(stderr, "%.4X: %.2X ", Ip - 1, op);
        switch (op) {
            default:
                fprintf(stderr, "???\n", op);
                abort();
        }
    }
}

void bc(uint8_t b) {
    assert(Cp < Msz);
    M[Cp++] = b;
}

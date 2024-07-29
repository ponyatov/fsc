#include "fsc.hpp"

int main(int argc, char *argv[]) {  //
    arg(0, argv[0]);
    for (int i = 1; i < argc; i++) {  //
        arg(i, argv[i]);
    }
    bc((byte)cmd::nop);
    bc((byte)cmd::halt);
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
        fprintf(stderr, "\n%.4X: %.2X ", Ip - 1, op);
        switch ((cmd)op) {
            case cmd::nop:
                nop();
                break;
            case cmd::halt:
                halt();
                break;
            default:
                fprintf(stderr, "???\n", op);
                abort();
        }
    }
}

void bc(byte b) {
    assert(Cp < Msz);
    M[Cp++] = b;
}

void nop() {  //
    fprintf(stderr, "nop");
}

void halt() {
    fprintf(stderr, "halt\n");
    exit(0);
}

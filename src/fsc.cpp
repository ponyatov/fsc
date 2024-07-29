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

uint8_t M[Msz];
uint16_t Cp = 0;
uint16_t Ip = 0;

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

typedef struct Buffer64 { char f1[64]; } Buffer64;

// declare the same function multiple type to avoid
// varargs limits with native interop

Buffer64 GetBuffer64Ret(void) {
	Buffer64 result = { };
	for (char i = 0; i < 64; i++) {
		result.f1[i] = 0b00100001;
	}
	return result;
}

Buffer64 GetBuffer64PRef(void) {
	Buffer64 result = { };
	for (char i = 0; i < 64; i++) {
		result.f1[i] = 0b00100001;
	}
	return result;
}


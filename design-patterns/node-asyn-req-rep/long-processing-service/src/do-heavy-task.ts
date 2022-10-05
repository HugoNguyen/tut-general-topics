const timeSleep = isNaN(+('' + process.env.DEFAULT_SLEEP_TIME_MS)) ? (1 * 60 * 1000) : +('' + process.env.DEFAULT_SLEEP_TIME_MS);

const sleep = (ms: number) => {
    return new Promise(resolve => setTimeout(resolve, ms));
}

export const doHeavyTask = async() => {
    console.log(`Begin processing: ${timeSleep/1000} s `);

    await sleep(timeSleep);

    console.log('End processing');
}
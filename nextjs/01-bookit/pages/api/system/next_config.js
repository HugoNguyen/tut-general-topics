import nc from 'next-connect';
import onError from '../../../middlewares/errors';
import getConfig from 'next/config';

const handler = nc({ onError });


handler.get((_, res)=> {
    res.status(200).json({
        success: true,
        config: getConfig(),
    });
});

export default handler;
 